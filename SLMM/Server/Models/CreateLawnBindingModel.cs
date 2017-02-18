using System.ComponentModel.DataAnnotations;

namespace SLMM.Server.Models
{
    public class CreateLawnBindingModel
    {
        [Required]
        public int StartX { get; set; }

        [Required]
        public int StartY { get; set; }

        [Required]
        public int SizeX { get; set; }

        [Required]
        public int SizeY { get; set; }
    }
}