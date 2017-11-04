

export class CityAccount {

    cityAccountId: number;
    cityExpenseId: number;
    expense: number;


    constructor() {
        this.cityAccountId = undefined;
        this.expense = 0;
        this.cityExpenseId = undefined;
    }

    static MapFromDetails(details: any): CityAccount {
        var cityAcount = new CityAccount();

        cityAcount.cityAccountId = details.cityAccountId;
        cityAcount.cityExpenseId = details.cityExpenseId;
        cityAcount.expense = details.expense;

        return cityAcount;
    }

}