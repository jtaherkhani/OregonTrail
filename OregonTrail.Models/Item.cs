using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OregonTrail.Models
{
    /// <summary>
    /// Stores item references for the game.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Database generated identifier used for lookup and foreign keys.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [Required()]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// The amount of points this scores the player by having it in their inventory.
        /// May also count against the player.
        /// </summary>
        [Range(-100, 100, ErrorMessage = "The item must have a point total between -100 and 100 points.")]
        public int Points { get; set; }

        /// <summary>
        /// Images are stored in blob Azure storage so the Image of the item stored on the item is a URL.
        /// </summary>
        public string Image { get; set; }
    }
}
