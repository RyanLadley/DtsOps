using dtso.core.Models;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class TransferManager
    {
        private ITransferRepository _transferRepository;

        public TransferManager(ITransferRepository transferRepo)
        {
            _transferRepository = transferRepo;
        }

        public int AddTransfer(Transfer transfer)
        {
            var transferId = _transferRepository.Add(transfer.MapToEntity());

            return transferId;
        }

        public List<Transfer> GetAccountTransfers(AccountNumberTemplate accountNumber, bool getFrom = true, bool getTo = true)
        {
            var transfers = new List<Transfer>();

            if (getFrom)
            {
                foreach (var transfer in _transferRepository.GetAccountTransfersFrom(accountNumber.AccountNumber.Value, accountNumber.SubNo, accountNumber.ShredNo))
                {
                    //We are taking away from this account, so the amount is negative
                    var fromTransfer = Transfer.MapFromEntity(transfer);
                    fromTransfer.Amount *= -1;

                    transfers.Add(fromTransfer);
                }
            }

            if (getTo)
            {
                foreach (var transfer in _transferRepository.GetAccountTransfersTo(accountNumber.AccountNumber.Value, accountNumber.SubNo, accountNumber.ShredNo))
                {
                    transfers.Add(Transfer.MapFromEntity(transfer));
                }
            }

            return transfers;
        }
        

    }
}
