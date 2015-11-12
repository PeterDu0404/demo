using System;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;

namespace Audit.Entity
{
    [Serializable]
    public class CoefficientEntity :NewClass
    {
        public CoefficientEntity()
        { }

        #region Field Members

        private string m_province; //                    

        public string province
        {
            get { return m_province; }
            set { m_province = value; NotifyPropertyChanged("province"); }
        }
        private string m_city; //          

        public string city
        {
            get { return m_city; }
            set { m_city = value; this.NotifyPropertyChanged("city"); }
        }
        private string m_season; //     

        public string season
        {
            get { return m_season; }
            set { m_season = value; this.NotifyPropertyChanged("season"); }
        }
        private decimal m_coefficient; //     

        public decimal coefficient
        {
            get { return m_coefficient; }
            set { m_coefficient = value; this.NotifyPropertyChanged("coefficient"); }
        }
        #endregion
    }
}
