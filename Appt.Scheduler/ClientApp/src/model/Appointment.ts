class Appointment {
    id: number
    personId: number
    date: Date
    type: string

    constructor(id: number, personId: number, date: Date, type: string) {
        this.id = id
        this.personId = personId
        this.date = date
        this.type = type
    }
}

export default Appointment;

//appointmentType: "exam"
//date: "0001-01-01T00:00:00"
//id: 1
//person: null
//personId: 2