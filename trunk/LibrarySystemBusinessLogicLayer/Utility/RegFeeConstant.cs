using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystemBusinessLogicLayer.Utility
{
    public class RegFeeConstant
    {
        private readonly decimal basic_Citizen= new Decimal(0.00);
        private readonly decimal basic_SPR = new Decimal(10.00);
        private readonly decimal basic_Foriegner = new Decimal(20.00);

        private readonly decimal premium_Citizen = new Decimal(10.00);
        private readonly decimal premium_SPR = new Decimal(20.00);
        private readonly decimal premium_Foriegner = new Decimal(30.00);

        private readonly decimal premiumPlus_Citizen = new Decimal(20.00);
        private readonly decimal premiumPlus_SPR = new Decimal(30.00);
        private readonly decimal premiumPlus_Foriegner = new Decimal(50.00);


        public decimal Basic_Citizen
        {
            get{ return basic_Citizen;}
        }
        
        public decimal Basic_SPR
        {
            get { return basic_SPR; }
        }

        public decimal Basic_Foriegner
        {
            get { return basic_Foriegner; }
        }

        public decimal Premium_Citizen
        {
            get { return premium_Citizen; }
        }

        public decimal Premium_SPR
        {
            get { return premium_SPR; }
        }

        public decimal Premium_Foriegner
        {
            get { return premium_Foriegner; }
        }

        public decimal PremiumPlus_Citizen
        {
            get { return premiumPlus_Citizen; }
        }

        public decimal PremiumPlus_SPR
        {
            get { return premiumPlus_SPR; }
        }

        public decimal PremiumPlus_Foriegner
        {
            get { return premiumPlus_Foriegner; }
        } 
    }
}
