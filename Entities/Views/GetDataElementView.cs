using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Views
{
    public class GetDataElementView:IView
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int SoldProduct { get; set; }
        public int DataSetId { get; set; }
    }
}
