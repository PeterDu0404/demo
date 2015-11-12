using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using Caliburn.Micro;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.ComponentModel.Composition;
using MessageBox = System.Windows.Forms.MessageBox;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;
using Audit.Ui;
using System.Collections.Generic;
using Audit.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Audit.Ui
{
    [Export(typeof(MainLoginViewModel))]
    public class MainLoginViewModel : Conductor<object>
    {
        readonly IWindowManager windowManager;

        [ImportingConstructor]
        public MainLoginViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
            this.entity = new CoefficientEntity();
        }

        private CoefficientEntity m_entity;
        public CoefficientEntity entity
        {
            get { return m_entity; }
            set
            {
                m_entity = value;
                this.NotifyOfPropertyChange("entity");
            }
        }

        public MainLoginViewModel()
        {
        }

        public void MinWindow()
        {
            MainLoginView main = (MainLoginView)GetView();

            main.Dispatcher.BeginInvoke(new System.Action(() =>
            {
                main.WindowState = System.Windows.WindowState.Minimized;
            }));
        }

        public void Check()
        {
            this.entity.coefficient =
            this.GetCoefficient(this.entity.province, this.entity.city, this.entity.season);
        }

        public void Cancel()
        {
            MainLoginView main = (MainLoginView)GetView();

            main.Dispatcher.BeginInvoke(new System.Action(() =>
            {
                main.Close();
            }));
            Application.Current.Shutdown();           
        }

        public void Closing(object param)
        {
            
        }

        List<CoefficientEntity> list;
        public void Loaded(object param)
        {
            string path =
            System.AppDomain.CurrentDomain.BaseDirectory + "json.txt";

            string text = System.IO.File.ReadAllText(path, Encoding.GetEncoding("GB2312"));

            JArray data = (JArray)JsonConvert.DeserializeObject(text);

            if (data != null)
            {
                list = (List<CoefficientEntity>)data.ToObject(new List<CoefficientEntity>().GetType());
            }
        }

        public decimal GetCoefficient(string province, string city, string season)
        {
            //if (list != null)
            {
                CoefficientEntity entity = list.FirstOrDefault(c=>c.province == province && c.city == city && c.season == season);

                if (entity == null)
                {
                    entity = list.FirstOrDefault(c => c.province == province && c.season == season);
                    if (entity == null)
                    {
                        entity = list.FirstOrDefault(c => c.province == "全国" && c.season == season);

                        if (entity == null)
                        {
                            entity = list.FirstOrDefault(c => c.province == province && c.city == city && c.season == "");
                            if (entity == null)
                            {
                                entity = list.FirstOrDefault(c => c.province == province && c.season == "");

                                if (entity == null)
                                {
                                    entity = list.FirstOrDefault(c => c.province == "全国" && c.season == "");
                                }
                            }
                        }
                    }
                }
            return entity.coefficient;
            }

            
        }
    }
}
