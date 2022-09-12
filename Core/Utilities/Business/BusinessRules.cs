using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    //BUSINESS KATMANINA ATİ İŞ KURALLARINI BURADA BELİRLİYORUZ.
    //Burada belirlememizin nedeni projemizin başka yerlerinde de bu kuralları kullanabilme ihtimalinin olmasıdır.
    public class BusinessRules
    {
        //params ifadesi ile IResult tipinde birden fazla iş kuralını buraya gönderebiliyoruz ve
        //params ile gönderdiğimiz kurallar IResult tipinde bir list oluyor.
        //logics : iş kuralı
        public static IResult Run(params IResult[] logics)
        {
            //tüm kuralları gez
            foreach (var logic in logics)
            {
                //kurala uymayan varsa
                if (!logic.Success)
                {
                    //uymayan kuralı döndür
                    return logic;
                }
            }

            return null;
        }
    }
}
