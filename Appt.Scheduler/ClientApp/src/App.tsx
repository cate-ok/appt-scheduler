import * as React from 'react';
//import './App.css';
import Navbar from './components/Navbar/Navbar';
import Appointment from './model/Appointment';

interface AppProps { }

interface AppState {
    appointments: Appointment[],
    selectedPage: number
}

class App extends React.Component<AppProps, AppState> {
    pages: string[];

    constructor(props: any) {
        super(props);
        this.pages = ['New appointment', 'Appointments', 'Test'];
        this.state = {
            appointments: [],
            selectedPage: 0
        };
        this.pageSelect = this.pageSelect.bind(this);
    }

    componentDidMount() {
        fetch('/appointment')
            .then(res => res.json())
            .then((data) => {
                let appts = []
                for (var i = 0; i < data.length; i++) {
                    appts.push(new Appointment(data[i].id, data[i].personId, data[i].date, data[i].appointmentType));
                }

                this.setState({ appointments: appts })
            })
            .catch(console.log)
    }

    pageSelect(index: number) {
        this.setState({ selectedPage: index });
    } 

    render() {
        let content;
        if (this.pages[this.state.selectedPage] == 'New appointment') {
            content = <div>Add new appointment'</div>;
        }
        else {
            content = <div>Your appointments:</div>;
        }
        return (
            <div className="App">
                <Navbar pages={this.pages} onSelect={ this.pageSelect} />
                <div>
                    {content}
                </div>

            </div>
        );
    }
}

export default App;