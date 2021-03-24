using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class FakeCard : IEntity
    {
        public int Id { get; set; }
        public string NameOnTheCard { get; set; }
        public string CardNumber { get; set; }
        public string CardCvv { get; set; }
        public string ExpirationDate { get; set; }
        public decimal MoneyInTheCard { get; set; }
    }
}
