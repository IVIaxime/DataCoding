using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.Coders
{
    public interface IEncodable
    {
        void Encode();

        double GetEncodingPrice();

        double GetCompressionCoeff();
    }
}
