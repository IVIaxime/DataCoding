using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Coders
{
    public interface IEncodable
    {
        void Encode();

        double GetEncodingPrice();

        double GetCompressionCoeff();
    }
}
