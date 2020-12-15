using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
namespace CookWebUtility
{

    public struct sKeyValue
    {
        public string sKey;
        public string sValue;
        public Boolean bHasChildren;
        public string sAttributes;
        public Boolean bProduct;
    }
    public class sNode
    {

        public string sUnid;
        public Boolean bChildren;
        public Boolean isSHeets;
        public string val;
        public ArrayList aColl;
        public string sParent;
        public sNode(string val)
        {
            sUnid = val;
            aColl = new ArrayList();
            aColl.Clear();
        }
    }
    public class sViewEntry
    {
        public ArrayList aList;

        public sViewEntry()
        {
            aList = new ArrayList();
            aList.Clear();

        }


    }

    public partial class xml_instance
    {
        public string sXmlClean;
        public sViewEntry sView;
        private bool bProcFlag;
        public XmlDocument xDoc;

        private sNode s;
        private string sCurrentParent;




        public void LoadXDoc()
        {

            try
            {
                xDoc = new XmlDocument();
                xDoc.LoadXml(sXmlClean);
            }
            catch (XmlException ex)
            {
                //  Exception ex = new Exception("Error on XML Load");
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("General Error");
            }


        }
        public void xml_Configure()
        // This method reads the nodes from the list
        {
            try
            {
                //      xl = new cXLWorker(@"e:\ccidev\testCall2.xlsm");




                sView = new sViewEntry();
                int iCnt = 0;
                bProcFlag = false;
                XmlNodeList nodes;
                nodes = xDoc.SelectNodes("//User");
                foreach (XmlNode n in nodes)
                {
                    s = new sNode("");
                    s.val = n.InnerXml.ToString();
                    iCnt += 1;
                    //    if (iCnt >= 5) break;
                    LoopThruChildren(n);
                    sView.aList.Add(s);
                }

            }


            catch (XmlException ex)
            {
                //  Exception ex = new Exception("Error on XML Load");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void LoopThruChildren(XmlNode rootnode)
        {

            if (rootnode.Name == "unid")
            {
                Debug.Print("Top Level User ID");
                Debug.Print("Got a Hit!");
                s.sUnid = rootnode.Value;
                s.val = rootnode.InnerXml.ToString();


            }


            foreach (XmlNode x in rootnode.ChildNodes)
            {
                LoopThruChildren(x, ref s);


            };

        }
        private void LoopThruChildren(XmlNode rootnode, ref sNode s)
        {
            Debug.Print(rootnode.Name);
            int iEvalChildren = 0;
            if (rootnode.Name == "unid")
            {
                // Got the unid
                s.sUnid = rootnode.InnerText;

            }
            if (rootnode.Attributes != null)
            {
                foreach (XmlAttribute a in rootnode.Attributes)
                {
                    if (a.Name == "product" && a.Value == "Y")
                    {
                        s.isSHeets = true;

                    }
                }
                if (sCurrentParent == "" && rootnode.Name == "sheets")
                {
                    sKeyValue sMystruc = new sKeyValue();
                    sMystruc.sKey = rootnode.Name;
                    sMystruc.bHasChildren = true;
                    sMystruc.sValue = rootnode.InnerXml;
                    sMystruc.sAttributes = "";
                    //     if (rootnode.Attributes["Product"].Value == "Y")
                    //          s.isSHeets = true;
                    if (rootnode.Attributes != null)
                    {
                        foreach (XmlAttribute att in rootnode.Attributes)
                        {
                            sMystruc.sAttributes += att.Value + "$";
                        }
                    }
                    s.aColl.Add(sMystruc);
                    sCurrentParent = rootnode.Name;
                }
                foreach (XmlNode x in rootnode.ChildNodes)
                {
                    Debug.Print("The name is {0}, value is {1}", x.ParentNode.Name, x.InnerText);
                    // mjm 21 Dec 2010
                    // if there is no value just blow through to the next one.

                    if (x.InnerText == null) continue;
                    iEvalChildren = x.ChildNodes.Count;

                    if (iEvalChildren > 0) iEvalChildren = 1;
                    foreach (XmlAttribute a in rootnode.Attributes)
                    {
                        if (a.Name == "Product" && a.Value == "Y")
                        {
                            s.isSHeets = true;

                        }
                    }

                    switch (iEvalChildren)
                    {
                        case 1:
                            // store the Parent in the collection as an anchor

                            Debug.Print("Has Children");



                            sNode s1 = new sNode("");
                            s1.sUnid = s.sUnid;
                            s1.val = x.InnerXml.ToString();
                            s1.isSHeets = s.isSHeets;
                            s1.sParent = sCurrentParent;
                            s.bChildren = true;
                            LoopThruChildren(x, ref s1);
                            s.aColl.Add(s1);
                            break;







                        default:
                            if (x.ChildNodes.Count == 0)
                            {
                                sCurrentParent = "";
                                sKeyValue sMystruc1 = new sKeyValue();
                                if (s.isSHeets == true)
                                    sMystruc1.bProduct = true;
                                sMystruc1.sKey = x.ParentNode.Name;
                                sMystruc1.sValue = x.InnerText;
                                sMystruc1.sAttributes = "";
                                if (x.Attributes != null)
                                {
                                    foreach (XmlAttribute att in x.Attributes)
                                    {
                                        sMystruc1.sAttributes += att.Value + "$";
                                    }
                                }
                                s.aColl.Add(sMystruc1);
                            }
                            break;

                    }


                }





                foreach (XmlNode x in rootnode.ChildNodes)
                {
                    LoopThruChildren(x, ref s);
                }
            }
        }

    }
}
