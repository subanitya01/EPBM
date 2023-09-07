using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPBM.Models
{
    public class Mesyuarat
    {
        [Required(ErrorMessage = "Sila Pilih Jenis Mesyuarat")]
        public string Jenis { get; set; }

        [Required(ErrorMessage = "Sila Isi Bil. Mesyuarat")]
        public string Bil { get; set; }

        [Required(ErrorMessage = "Sila Isi Tahun Bil. Mesyuarat")]
        public string Tahun { get; set; }

        [Required(ErrorMessage = "Sila Isi Tarikh Mesyuarat")]
        public string Tarikh { get; set; }
    }
}