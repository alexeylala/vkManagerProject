using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows;
using System.Reflection;

namespace vkManageFinal
{
    public class LoadToXML
    {
        XmlDocument doc = new XmlDocument();
        XmlTextWriter xmlWriter = null;

        public void LoadToFile(string path ,List<MyUser> users) {

            try {

                xmlWriter = new XmlTextWriter(path, Encoding.UTF8);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("users");
                xmlWriter.WriteEndElement();
                xmlWriter.Close();

                doc.Load(path);

                foreach (MyUser user in users)
                {

                    XmlElement userElement = doc.CreateElement("user");

                    foreach (PropertyInfo propertyInfo in user.GetType().GetProperties())
                    {

                        XmlElement elem = doc.CreateElement(propertyInfo.Name);
                        elem.InnerText = propertyInfo.GetValue(user).ToString();
                        userElement.AppendChild(elem);

                    }//foreach

                    doc.ChildNodes[1].AppendChild(userElement);

                }//foreach

                doc.Save(path);

            }//try

            catch(Exception ex){

                MessageBox.Show(ex.Message);

            }//catch


        }

        public void ExtractFromFile(string path, out List<MyUser> destination) {

            destination = new List<MyUser>();

            try {

                doc.Load(path);

                XmlNodeList list = doc.SelectNodes("//user");

                if (list.Count == 0)
                {
                    MessageBox.Show("Обраний файл порожній!");
                }//if

                else { 
                
                    foreach(XmlNode node in list ){

                        MyUser user = new MyUser();

                        foreach (XmlNode cnode in node.ChildNodes) {

                            string value = cnode.InnerXml;

                            user.GetType().GetProperty(cnode.Name).SetValue(user, value);

                        }//foreach

                        destination.Add(user);

                    }//foreach

                }//else

            }//try

            catch(Exception ex){
                MessageBox.Show("Завантажити користувачів з файлу: " + path + " - не можливо!");
            }//catch

        }//ExtractFromFile

    }
}
