﻿using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class PredictionsEntity
    {
        [Key]
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int FeaturesDataId { get; set; }
        public FeaturesDataEntity Features { get; set; }
    }
}
