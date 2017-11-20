using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dtso.data.Repositories.Interfaces;
using System.Data.SqlClient;

namespace dtso.data.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private MainContext _context;

        public TransferRepository(MainContext context)
        {
            _context = context;
        }

        public int Add(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            _context.SaveChanges();

            return transfer.TransferId;
        }

        public Transfer Get(int TransferId)
        {
            throw new NotImplementedException();
        }

        public List<Transfer> GetAccountTransfersFrom(int AccountNumber, int? SubNo, int? ShredNo)
        {
            if (ShredNo.HasValue && SubNo.HasValue)
            {
                return _getShredTransfersFrom(AccountNumber, SubNo.Value, ShredNo.Value);
            }
            else if (SubNo.HasValue)
            {
                return _getSubTransfersFrom(AccountNumber, SubNo.Value);
            }
            else
            {
                return _getRootTransfersFrom(AccountNumber);
            }
        }

        private List<Transfer> _getRootTransfersFrom(int accountNumber)
        {
            var transfers =
                (from transfer in _context.Transfers
                 join vAccount in _context.vAccounts on transfer.FromAccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                 select transfer).Distinct()

                .Include(transfer => transfer.FromvAccount)
                .Include(transfer => transfer.TovAccount);

            return transfers.ToList();
        }

        private List<Transfer> _getSubTransfersFrom(int accountNumber, int SubNo)
        {
            var transfers =
                 (from transfer in _context.Transfers
                  join vAccount in _context.vAccounts on transfer.FromAccountId equals vAccount.AccountId
                  where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                  select transfer)

                .Include(transfer => transfer.FromvAccount)
                .Include(transfer => transfer.TovAccount);

            return transfers.ToList();
        }

        private List<Transfer> _getShredTransfersFrom(int accountNumber, int SubNo, int ShredNo)
        {
            var transfers =
                (from transfer in _context.Transfers
                 join vAccount in _context.vAccounts on transfer.FromAccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                    && vAccount.ShredNo == ShredNo
                 select transfer)

                .Include(transfer => transfer.FromvAccount)
                .Include(transfer => transfer.TovAccount);

            return transfers.ToList();
        }
        public List<Transfer> GetAccountTransfersTo(int AccountNumber, int? SubNo, int? ShredNo)
        {
            if (ShredNo.HasValue && SubNo.HasValue)
            {
                return _getShredTransfersTo(AccountNumber, SubNo.Value, ShredNo.Value);
            }
            else if (SubNo.HasValue)
            {
                return _getSubTransfersTo(AccountNumber, SubNo.Value);
            }
            else
            {
                return _getRootTransfersTo(AccountNumber);
            }
        }

        private List<Transfer> _getRootTransfersTo(int accountNumber)
        {
            var transfers =
                (from transfer in _context.Transfers
                 join vAccount in _context.vAccounts on transfer.ToAccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                 select transfer).Distinct()

                .Include(transfer => transfer.TovAccount)
                .Include(transfer => transfer.FromvAccount);

            return transfers.ToList();
        }

        private List<Transfer> _getSubTransfersTo(int accountNumber, int SubNo)
        {
            var transfers =
                 (from transfer in _context.Transfers
                  join vAccount in _context.vAccounts on transfer.ToAccountId equals vAccount.AccountId
                  where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                  select transfer)

                .Include(transfer => transfer.TovAccount)
                .Include(transfer => transfer.FromvAccount);

            return transfers.ToList();
        }

        private List<Transfer> _getShredTransfersTo(int accountNumber, int SubNo, int ShredNo)
        {
            var transfers =
                (from transfer in _context.Transfers
                 join vAccount in _context.vAccounts on transfer.ToAccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                    && vAccount.ShredNo == ShredNo
                 select transfer)

                .Include(transfer => transfer.TovAccount)
                .Include(transfer => transfer.FromvAccount);

            return transfers.ToList();
        }

    }
}
