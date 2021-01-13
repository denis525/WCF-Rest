using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfAvtosalonRest.Model;

namespace WcfAvtosalonRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAvtosalonService" in both code and config file together.
    [ServiceContract]
    public interface IAvtosalonService
    {
     
        [OperationContract]
        [WebGet(UriTemplate = "/avto/{id}", BodyStyle =WebMessageBodyStyle.Bare, ResponseFormat =WebMessageFormat.Xml)]
        Avto getAvto(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/avti", BodyStyle =WebMessageBodyStyle.Bare)]
        List<Avto> getAllAvte();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/avto", BodyStyle = WebMessageBodyStyle.Bare)]
        void addAvto(Avto avto);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/avto/{id}", BodyStyle = WebMessageBodyStyle.Bare)]
        void updateAvto(string id, Avto avto);


        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/avto/{id}")]
        void deleteAvto(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/izposoja/{id}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Xml)]
        Izposoja vrniIzposoje(int id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/izposoja/{id}")]
        void deleteIzposoja(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/izposoja", BodyStyle = WebMessageBodyStyle.Bare)]
        bool createIzposoja(Izposoja izposoja);


        [OperationContract]
        [WebGet(UriTemplate = "/izposoje", BodyStyle = WebMessageBodyStyle.Bare)]
        List<Izposoja> vrniIzposoje();

        [OperationContract]
        [WebGet(UriTemplate = "/narocnik/{id}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Xml)]
        Narocnik vrniNarocnike(int id);

        [OperationContract]
        void deleteNarocnik(string id);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/narocnik/{id}", BodyStyle = WebMessageBodyStyle.Bare)]
        void updateNarocnik(string id, Narocnik narocnik);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/narocnik", BodyStyle = WebMessageBodyStyle.Bare)]
        bool createNarocnik(Narocnik narocnik);


        [OperationContract]
        [WebGet(UriTemplate = "/narocniki", BodyStyle = WebMessageBodyStyle.Bare)]
        List<Narocnik> vrniNarocnike();



        /*[OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/avto/{id}")]
        void createAvto(string id);*/
    }

}
