using System.ComponentModel.DataAnnotations;

namespace TamagotchiData.Models;

public class Dragon
{
    public Guid DragonId { get; set; }
    [Required(ErrorMessage = "You must name your dragon!")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Your dragon's name must be between 1 and 15 characters")]
    public string Name { get; set; } = string.Empty;

    public decimal Age { get; set; }

    public AgeGroup AgeGroup
    {
        get
        {
            return Age switch
            {
                <= 2 => AgeGroup.Baby,
                <= 10 => AgeGroup.Child,
                <= 20 => AgeGroup.Teen,
                <= 60 => AgeGroup.Adult,
                _ => AgeGroup.Senior
            };
        }
    }

    public bool IsAlive { get; set; } = true;

    public int Feedometer { get; set; }

    public int Happiness { get; set; }
}