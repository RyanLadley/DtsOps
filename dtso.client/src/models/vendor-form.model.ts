import { MaterialNew } from './material-new.model';
import { MaterialKnown } from './material-known.model'

export class VendorForm {

    vendorName: number;
    contractNumber: string;
    contractStart: Date;
    contractEnd: Date;
    pointOfContact: string;
    phoneNumber: string;
    email: string;
    website: string;


    newMaterial: MaterialNew[];
    knownMaterial: MaterialKnown[]

    constructor() {
        this.newMaterial = [];
        this.knownMaterial = [];
    }

}