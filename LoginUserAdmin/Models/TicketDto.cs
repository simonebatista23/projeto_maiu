using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginUserAdmin.Models
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string desc { get; set; }
        public int? Id_category { get; set; }
        public int? Id_status { get; set; }
        public DateTime Open_datetime { get; set; }
        public int? Priority_level { get; set; }
        public int? Id_user { get; set; }
    }
}
