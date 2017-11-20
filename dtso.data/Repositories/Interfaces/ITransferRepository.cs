using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        Transfer Get(int TransferId);
        int Add(Transfer transfer);
        List<Transfer> GetAccountTransfersFrom(int AccountNumber, int? SubNo, int? ShredNo);
        List<Transfer> GetAccountTransfersTo(int AccountNumber, int? SubNo, int? ShredNo);
    }
}
