using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class PairingViewModel
    {
        public int Id { get; set; }
        [Required]
        public int SantaId { get; set; }
        [Required]
        public int RecipientId { get; set; }
    }
}
