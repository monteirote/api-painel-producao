﻿using api_painel_producao.Models.Enums;
using api_painel_producao.Validators;
using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.ViewModels {

    public class CreateMaterialViewModel {

        [Required]
        public string Name { get; set; } = string.Empty;


        [Required]
        public string Description { get; set; } = string.Empty;


        [Required]
        [EnumValidation(typeof(MaterialType), ErrorMessage = "Action failed: the value of 'Type' is not valid.")]
        public string Type { get; set; } = string.Empty;


        [Required]
        [EnumValidation(typeof(MeasurementUnit), ErrorMessage = "Action failed: the value of 'MeasurementUnit' is not valid.")]
        public string MeasurementUnit { get; set; } = string.Empty;


        [Required]
        public decimal ValueByUnit { get; set; } = 0;

    }
}