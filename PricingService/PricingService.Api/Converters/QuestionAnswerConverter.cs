using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PricingService.Api.Commands.Dto;

namespace PricingService.Api.Converters;

internal class QuestionAnswerConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsAssignableFrom(typeof(QuestionAnswer));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var target = Create(jsonObject);
        serializer.Populate(jsonObject.CreateReader(), target);
        return target;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is QuestionAnswer questionAnswer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("questionCode");
            serializer.Serialize(writer, questionAnswer.QuestionCode);
            writer.WritePropertyName("questionType");
            serializer.Serialize(writer, questionAnswer.QuestionType);
            writer.WritePropertyName("answer");
            serializer.Serialize(writer, questionAnswer.GetAnswer());
            writer.WriteEndObject();
        }
    }

    private static QuestionAnswer Create(JObject jsonObject)
    {
        var questionCode = jsonObject["questionCode"]?.ToString() ?? "";
        var answerToken = jsonObject["answer"];
        var questionTypeToken = jsonObject["questionType"];

        // Auto-detect when questionType is missing
        if (questionTypeToken == null)
        {
            if (answerToken != null && (answerToken.Type == JTokenType.Integer || answerToken.Type == JTokenType.Float))
                return new NumericQuestionAnswer { QuestionCode = questionCode, Answer = answerToken.Value<decimal>() };
            return new ChoiceQuestionAnswer { QuestionCode = questionCode, Answer = answerToken?.ToString() ?? "" };
        }

        // Normalize: PolicyService uses "Numeric", PricingService uses "Number"
        var typeStr = questionTypeToken.ToString();
        if (typeStr.Equals("Numeric", StringComparison.OrdinalIgnoreCase))
            typeStr = "Number";

        switch (typeStr)
        {
            case "Text":
                return new TextQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = answerToken?.ToString() ?? ""
                };
            case "Number":
                return new NumericQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = answerToken?.Value<decimal>() ?? 0M
                };
            case "Choice":
                return new ChoiceQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = answerToken?.ToString() ?? ""
                };
            default:
                // Fallback: auto-detect from token type
                if (answerToken != null && (answerToken.Type == JTokenType.Integer || answerToken.Type == JTokenType.Float))
                    return new NumericQuestionAnswer { QuestionCode = questionCode, Answer = answerToken.Value<decimal>() };
                return new ChoiceQuestionAnswer { QuestionCode = questionCode, Answer = answerToken?.ToString() ?? "" };
        }
    }
}
