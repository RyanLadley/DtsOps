import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
        name: 'accountName'
})
export class AccountNamePipe implements PipeTransform {
    transform(account: any): string {
        if (!account) return null;

        if (account.shredNo != null) {
            return [account.accountNumber, account.subNo, account.shredNo].join('-')
        }
        else if (account.subNo != null) {
            return [account.accountNumber, account.subNo].join('-')
        }
        else {
            return account.accountNumber
        }
    }
}