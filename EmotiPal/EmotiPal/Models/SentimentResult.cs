using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmotiPal.Models
{
    public class SentimentResult : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        
        [Required]
        [MapTo("_partition")]
        public string PartitionKey { get; set; }

        [Required]
        public string AnalysedText { get; set; }

        [Required]
        public string Sentiment { get; set; }
    }
}
