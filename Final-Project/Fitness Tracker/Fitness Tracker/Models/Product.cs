using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // FK + Navigation (many Products -> one Category)
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string ResolvedImageUrl
        {
            get
            {
                var key = (Name ?? string.Empty).Trim().ToLowerInvariant();
                var mapped = key switch
                {
                    "yoga mat" => "/images/Astral Mat.jpg",
                    "dumbbells set" => "/images/dumbells.png",
                    "resistance bands" => "/images/Resisitance Bands.pgn.jpg",
                    "protein powder" => "/images/protien powder.png",
                    "water bottle" => "/images/Water bottle.png",
                    "kettlebell 20lb" => "/images/random Equipment.png",
                    "jump rope" => "/images/Lifting Straps.png",
                    "foam roller" => "/images/random Equipment.png",
                    "pull-up bar" => "/images/PullUp Bar.png",
                    "adjustable bench" => "/images/dumbell equipment.png",
                    "creatine monohydrate" => "/images/Creatine.png",
                    "pre-workout" => "/images/Pre Workout.png",
                    "bcaa powder" => "/images/Protien Bar.png",
                    "multivitamin" => "/images/MultiVitamin.png",
                    "adjustable rope" => "/images/Lifting Straps.png",
                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(mapped))
                {
                    return mapped;
                }

                if (!string.IsNullOrWhiteSpace(ImageUrl))
                {
                    return ImageUrl;
                }

                return "/images/product-placeholder.svg";
            }
        }

        // Computed property for URL slug
        public string Slug => Name?.Replace(' ', '-').ToLower() ?? string.Empty;
    }
}
