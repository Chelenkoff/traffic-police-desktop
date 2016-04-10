using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        // TODO: Add your service operations here
      [OperationContract]
        int UpdateUser(User user);

      [OperationContract]
      User GetUserByIdAndPass(string id, string password);

      [OperationContract]
        //Getting user data with nullified pass
      User GetReadOnlyUserById(string id);

      [OperationContract]
      int InsertUser(User usr);

      [OperationContract]
      int InsertCat(Categories categ);

      [OperationContract]
       int RegisterDriverOwner(DriverOwner drOwner);
      [OperationContract]
      DriverOwner GetDriverOwnerById(string id);

      [OperationContract]
      int removePenalty(Penalty pen);


       
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
   

}
