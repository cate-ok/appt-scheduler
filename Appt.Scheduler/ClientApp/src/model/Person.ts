//import Move from "./Move";

class Person {
    id: number
    firstName: string
    lastName: string
    phone: string | undefined
    email: string | undefined

    constructor(id: number, fname: string, lname: string, phone?: string, email?: string) {
        this.id = id;
        this.firstName = fname;
        this.lastName = lname;
        this.phone = phone;
        this.email = email;
    }

    getFullName() {
        return this.firstName + " " + this.lastName;
    }
}

export default Person;