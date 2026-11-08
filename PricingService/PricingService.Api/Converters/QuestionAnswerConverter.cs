using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PricingService.Api.Commands.Dto;

namespace PricingService.Api.Converters;

internal class QuestionAnswerConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(QuestionAnswer).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var jsonObject = JObject.Load(reader);
        return Create(jsonObject);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is QuestionAnswer questionAnswer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("questionCode");
            serializer.Serialize(writer, questionAnswer.QuestionCode);
            writer.WritePropertyName("questionType");
            serializer.Serialize(writer, questionAnswer.QuestionType.ToString());
            writer.WritePropertyName("answer");
            serializer.Serialize(writer, questionAnswer.GetAnswer());
            writer.WriteEndObject();
        }
    }

    private static QuestionAnswer Create(JObject jsonObject)
    {
        var questionCode = (jsonObject.GetValue("questionCode", StringComparison.OrdinalIgnoreCase)
                           ?? jsonObject.GetValue("QuestionCode", StringComparison.OrdinalIgnoreCase))?.ToString() ?? "";

        var questionTypeToken = jsonObject.GetValue("questionType", StringComparison.OrdinalIgnoreCase)
                                ?? jsonObject.GetValue("QuestionType", StringComparison.OrdinalIgnoreCase);

        var answerToken = jsonObject.GetValue("answer", StringComparison.OrdinalIgnoreCase)
                          ?? jsonObject.GetValue("Answer", StringComparison.OrdinalIgnoreCase);

        var typeStr = questionTypeToken?.ToString()?.Trim();
        if (string.Equals(typeStr, "Numeric", StringComparison.OrdinalIgnoreCase))
            typeStr = "Number";

        // If type is explicitly specified
        if (!string.IsNullOrEmpty(typeStr))
        {
            if (string.Equals(typeStr, "Number", StringComparison.OrdinalIgnoreCase))
            {
                return new NumericQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = ParseDecimal(answerToken)
                };
            }

            if (string.Equals(typeStr, "Text", StringComparison.OrdinalIgnoreCase))
            {
                return new TextQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = answerToken?.ToString() ?? ""
                };
            }

            if (string.Equals(typeStr, "Choice", StringComparison.OrdinalIgnoreCase))
            {
                return new ChoiceQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = answerToken?.ToString() ?? ""
                };
            }
        }

        // Auto-detect if questionType is missing or unknown
        if (answerToken != null)
        {
            if (answerToken.Type == JTokenType.Integer || answerToken.Type == JTokenType.Float)
            {
                return new NumericQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = ParseDecimal(answerToken)
                };
            }

            var rawStr = answerToken.ToString();
            if (decimal.TryParse(rawStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedVal)
                && (questionCode.StartsWith("NUM_", StringComparison.OrdinalIgnoreCase)
                    || questionCode.StartsWith("AREA", StringComparison.OrdinalIgnoreCase)
                    || questionCode.Contains("COUNT", StringComparison.OrdinalIgnoreCase)))
            {
                return new NumericQuestionAnswer
                {
                    QuestionCode = questionCode,
                    Answer = parsedVal
                };
            }

            return new ChoiceQuestionAnswer
            {
                QuestionCode = questionCode,
                Answer = rawStr
            };
        }

        return new ChoiceQuestionAnswer
        {
            QuestionCode = questionCode,
            Answer = ""
        };
    }

    private static decimal ParseDecimal(JToken token)
    {
        if (token == null)
            return 0M;

        if (token.Type == JTokenType.Integer || token.Type == JTokenType.Float)
            return token.Value<decimal>();

        if (decimal.TryParse(token.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            return result;

        return 0M;
    }
}
