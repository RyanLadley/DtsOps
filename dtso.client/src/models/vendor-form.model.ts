import { MaterialNew } from './material-new.model';
import { MaterialKnown } from './material-known.model'

export class VendorForm {

    vendorId: number
    vendorName: string;
    contractNumber: string;
    contractStart: Date;
    contractEnd: Date;
    pointOfContact: string;
    phoneNumber: string;
    email: string;
    website: string;
    active: boolean;

    newMaterial: MaterialNew[];
    knownMaterial: MaterialKnown[]

    constructor() {
        this.newMaterial = [];
        this.knownMaterial = [];
    }

    static MapFromDetails(details: any) {
        var form = new VendorForm();

        form.vendorId = details.vendorId;
        form.vendorName = details.name;
        form.contractNumber = details.contractNumber;
        form.contractEnd = details.contractEnd;
        form.contractStart = details.contractStart;
        form.pointOfContact = details.pointOfContact;
        form.phoneNumber = details.phoneNumber;
        form.email = details.email;
        form.website = details.website;
        form.active = details.active;
        return form;
    }

}