import { Component, OnInit } from '@angular/core';
import { ServerRequest } from '../../services/index';

@Component({
    selector: 'overview',
    templateUrl: './overview.template.html'
})
export class OverviewComponent implements OnInit {
    collapse: boolean;
    accounts: any;

    constructor(private _server: ServerRequest) {

    }

    ngOnInit() {
        this.collapse = false;
        this.accounts = this.getAccounts();

        this.toggleCollapse();
    }

    toggleCollapse() {
        if (this.accounts) {
            this.collapse = !this.collapse;

            for (var i = 0; i < this.accounts.length; i++) {
                this.accounts[i].hideChildren = this.collapse;

                for (var j = 0; j < this.accounts[i].childAccounts.length; j++) {
                    this.accounts[i].childAccounts[j].hideChildren = this.collapse;
                }
            }
        }
    }


    getAccounts() {
        this._server.get('api/account/overview').subscribe(
            response => { this.accounts = response },
            error => { }
        )
    }

    /*accounts: any = [
        {
            "accountId" : 1,
            "accountNumber": 12345,
            "subNo": null,
            "shredNo": null,
            "isActive": false,
            "description": "Marcia Bass",
            "expedituresToDate": -1060.94,
            "transfers": -1307.5912,
            "annualBudget": -1569.86,
            "totalBudget": -1218.53,
            "remainingBalance": -1392.8,
            "childAccounts": [
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 0,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Hoffman Galloway",
                    "expedituresToDate": -1301.56,
                    "transfers": -8497.534,
                    "annualBudget": -79.48,
                    "totalBudget": -1423.51,
                    "remainingBalance": 19.3,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": false,
                            "description": "Fitzgerald Bradshaw",
                            "expedituresToDate": -419.78,
                            "transfers": -74.7098,
                            "annualBudget": -1361.37,
                            "totalBudget": -1627.34,
                            "remainingBalance": -202
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": true,
                            "description": "Elaine Rodgers",
                            "expedituresToDate": -780.27,
                            "transfers": -3685.8883,
                            "annualBudget": 93.37,
                            "totalBudget": 30.12,
                            "remainingBalance": -1120.16
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": false,
                            "description": "Jennings Frazier",
                            "expedituresToDate": -1719.96,
                            "transfers": -5599.9393,
                            "annualBudget": -1216.18,
                            "totalBudget": -1627.77,
                            "remainingBalance": -895.06
                        }
                    ]
                },
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 1,
                    "shredNo": null,
                    "isActive": true,
                    "description": "Mccray Brock",
                    "expedituresToDate": -297.13,
                    "transfers": -6774.2888,
                    "annualBudget": -487.69,
                    "totalBudget": -51.12,
                    "remainingBalance": -205.9,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": false,
                            "description": "Carol Cochran",
                            "expedituresToDate": -579.74,
                            "transfers": -758.8902,
                            "annualBudget": -1624.47,
                            "totalBudget": -1659.62,
                            "remainingBalance": -1686.47
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": false,
                            "description": "Alba Ball",
                            "expedituresToDate": -598.04,
                            "transfers": -1007.3458,
                            "annualBudget": -1510.72,
                            "totalBudget": -354.23,
                            "remainingBalance": -1069.97
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": false,
                            "description": "Key Floyd",
                            "expedituresToDate": -496.02,
                            "transfers": -2154.5459,
                            "annualBudget": 71.11,
                            "totalBudget": -1248.48,
                            "remainingBalance": -860.25
                        }
                    ]
                },
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 2,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Mcintosh Gamble",
                    "expedituresToDate": -590.06,
                    "transfers": -1463.3948,
                    "annualBudget": -943.29,
                    "totalBudget": 124.82,
                    "remainingBalance": -836.71,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": false,
                            "description": "Ladonna Mcconnell",
                            "expedituresToDate": -1325.78,
                            "transfers": -3492.7725,
                            "annualBudget": -215.79,
                            "totalBudget": -939.32,
                            "remainingBalance": -526.13
                        },
                        {
                            "accountId": 2,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": true,
                            "description": "Franco House",
                            "expedituresToDate": -83.82,
                            "transfers": -7421.9845,
                            "annualBudget": -1592.85,
                            "totalBudget": -975.66,
                            "remainingBalance": -589.81
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": true,
                            "description": "Duran Langley",
                            "expedituresToDate": -621.82,
                            "transfers": -5241.799,
                            "annualBudget": -883.41,
                            "totalBudget": 148.35,
                            "remainingBalance": -735.25
                        }
                    ]
                }
            ]
        },
        {
            "accountId": 4,
            "accountNumber": 12345,
            "subNo": null,
            "shredNo": null,
            "isActive": true,
            "description": "Lucas Cross",
            "expedituresToDate": -1618.6,
            "transfers": -7235.7164,
            "annualBudget": -216.93,
            "totalBudget": -1735.68,
            "remainingBalance": -920.46,
            "childAccounts": [
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 0,
                    "shredNo": null,
                    "isActive": true,
                    "description": "Johns Burgess",
                    "expedituresToDate": -370.76,
                    "transfers": -8293.5062,
                    "annualBudget": 167.13,
                    "totalBudget": -280.54,
                    "remainingBalance": -821.53,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": true,
                            "description": "Underwood Guerrero",
                            "expedituresToDate": -435.61,
                            "transfers": -3081.8502,
                            "annualBudget": -940.47,
                            "totalBudget": -887.68,
                            "remainingBalance": -260.86
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": false,
                            "description": "Erica Greene",
                            "expedituresToDate": -1057.95,
                            "transfers": -7521.0062,
                            "annualBudget": -1563.96,
                            "totalBudget": -1256.95,
                            "remainingBalance": -1658.9
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": true,
                            "description": "Gonzales Barber",
                            "expedituresToDate": -1774.43,
                            "transfers": -3397.489,
                            "annualBudget": -1358.26,
                            "totalBudget": -1687.51,
                            "remainingBalance": -708.31
                        }
                    ]
                },
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 1,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Carole Solomon",
                    "expedituresToDate": -1240.68,
                    "transfers": -4289.9729,
                    "annualBudget": -1268.7,
                    "totalBudget": -143.99,
                    "remainingBalance": 92.07,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": false,
                            "description": "Shirley Durham",
                            "expedituresToDate": -1414.04,
                            "transfers": -2490.3663,
                            "annualBudget": -314.82,
                            "totalBudget": -574.31,
                            "remainingBalance": -346.79
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": true,
                            "description": "Byrd Hoffman",
                            "expedituresToDate": -949.45,
                            "transfers": -5291.7542,
                            "annualBudget": -1294.57,
                            "totalBudget": -1195.91,
                            "remainingBalance": -938.69
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": false,
                            "description": "Juliet Osborne",
                            "expedituresToDate": -411.77,
                            "transfers": -7837.6407,
                            "annualBudget": -154.74,
                            "totalBudget": -1052,
                            "remainingBalance": -195.57
                        }
                    ]
                },
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 2,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Armstrong James",
                    "expedituresToDate": -1286.59,
                    "transfers": -4032.0358,
                    "annualBudget": -1440.15,
                    "totalBudget": -865.99,
                    "remainingBalance": -655.08,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": false,
                            "description": "Shepherd Ross",
                            "expedituresToDate": -1680.43,
                            "transfers": -302.6211,
                            "annualBudget": -242.64,
                            "totalBudget": -1495.47,
                            "remainingBalance": -50.95
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": false,
                            "description": "Dora Finch",
                            "expedituresToDate": -1783.78,
                            "transfers": -4153.9966,
                            "annualBudget": -1279.24,
                            "totalBudget": -932.14,
                            "remainingBalance": -790.56
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": false,
                            "description": "Cecelia Trevino",
                            "expedituresToDate": -1081.75,
                            "transfers": -4490.1571,
                            "annualBudget": -1261.37,
                            "totalBudget": -1637.81,
                            "remainingBalance": -340.04
                        }
                    ]
                }
            ]
        },
        {
            "accountId": 1,
            "accountNumber": 12345,
            "subNo": null,
            "shredNo": null,
            "isActive": false,
            "description": "Macias Banks",
            "expedituresToDate": -1794.33,
            "transfers": -3735.9074,
            "annualBudget": -1679.34,
            "totalBudget": -411.76,
            "remainingBalance": -1647.95,
            "childAccounts": [
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 0,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Nettie Burton",
                    "expedituresToDate": -1735.75,
                    "transfers": -8875.4808,
                    "annualBudget": -538.32,
                    "totalBudget": 23.41,
                    "remainingBalance": -1296.02,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": true,
                            "description": "Mary Alston",
                            "expedituresToDate": 138.18,
                            "transfers": -5923.8278,
                            "annualBudget": -394.4,
                            "totalBudget": -1706.41,
                            "remainingBalance": -845.7
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": false,
                            "description": "Nolan Gibbs",
                            "expedituresToDate": -688.77,
                            "transfers": -1052.6101,
                            "annualBudget": -12.59,
                            "totalBudget": -555.03,
                            "remainingBalance": -3.99
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": false,
                            "description": "Nadine Kirk",
                            "expedituresToDate": -697.43,
                            "transfers": -5180.9885,
                            "annualBudget": -287.99,
                            "totalBudget": 52.69,
                            "remainingBalance": 83.03
                        }
                    ]
                },
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 1,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Holmes Good",
                    "expedituresToDate": 1.28,
                    "transfers": -8843.8333,
                    "annualBudget": -1637.88,
                    "totalBudget": -181.17,
                    "remainingBalance": -1772.96,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": true,
                            "description": "Rocha Higgins",
                            "expedituresToDate": -1754.48,
                            "transfers": -5907.6873,
                            "annualBudget": -286.17,
                            "totalBudget": -420.37,
                            "remainingBalance": -1012.38
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": true,
                            "description": "Marisol Landry",
                            "expedituresToDate": -1307.22,
                            "transfers": -5938.911,
                            "annualBudget": -375.01,
                            "totalBudget": -1667.44,
                            "remainingBalance": -1165.57
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": false,
                            "description": "Brittany Estrada",
                            "expedituresToDate": -152.18,
                            "transfers": -1295.0488,
                            "annualBudget": 177.95,
                            "totalBudget": -944.55,
                            "remainingBalance": -363.9
                        }
                    ]
                },
                {
                    "accountId": 1,
                    "accountNumber": 12345,
                    "subNo": 2,
                    "shredNo": null,
                    "isActive": false,
                    "description": "Spencer Walsh",
                    "expedituresToDate": -1585.58,
                    "transfers": -2074.1208,
                    "annualBudget": -201.91,
                    "totalBudget": -753.41,
                    "remainingBalance": -624.92,
                    "childAccounts": [
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 0,
                            "shredNo": 0,
                            "isActive": true,
                            "description": "Woods Everett",
                            "expedituresToDate": -177.7,
                            "transfers": -1295.6898,
                            "annualBudget": -689.19,
                            "totalBudget": -163.29,
                            "remainingBalance": -1195.81
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 1,
                            "shredNo": 1,
                            "isActive": false,
                            "description": "Caldwell Blair",
                            "expedituresToDate": -1065.51,
                            "transfers": -1981.1235,
                            "annualBudget": -1455.12,
                            "totalBudget": 136.76,
                            "remainingBalance": -890.38
                        },
                        {
                            "accountId": 1,
                            "accountNumber": 12345,
                            "subNo": 2,
                            "shredNo": 2,
                            "isActive": true,
                            "description": "Lynch Rivas",
                            "expedituresToDate": -963.18,
                            "transfers": -8559.8337,
                            "annualBudget": -1344.76,
                            "totalBudget": -51.36,
                            "remainingBalance": -14.55
                        }
                    ]
                }
            ]
        }
    ]*/
}
