export interface Customer {
  id: number;
  name: string;
  phone: string;
  email: string;
  gender: string;
  customerGroup: string;
  customerNumber: string;
  dob?: Date;
  address: string;
  city: string;
  ward: string;
}
