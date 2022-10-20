﻿
using System;
using System.ComponentModel.DataAnnotations;
using VxFormGenerator.Components.Plain.Models;
using VxFormGenerator.Core;

namespace VxFormGeneratorDemoData
{
    public class FeedingSession
    {
        [Display(Name = "Kind of food")]
        public FoodKind KindOfFood { get; set; }
        
        [Display(Name = "Note")]
        [MinLength(5)]
        public string Note { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Start")]
        public DateTime Start { get; set; }
        [Display(Name = "End")]
        public DateTime End { get; set; }
        [Display(Name = "Throwing up")]
        public bool ThrowingUp { get; set; }

        [Display(Name = "Throwing up dict")]
        public ValueReferences<FoodKind> ThrowingUpDict { get; set; } = new();
        [Display(Name = "Color")]
        public VxColor Color { get; set; }
    }

    [Flags]
    public enum FoodKind
    {
        SOLID,
        LIQUID
    }
}
