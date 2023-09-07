using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sifreKasa.Data.Data
{
    public class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int AccountID { get; set; }

        [Required]
        public string SiteName { get; set; }

        [Required]
        public string AccountUserName { get; set; }

        [Required]
        public string AccountPassword { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public Users User { get; set; }
   
    }
}
