import * as React from 'react';
import './Navbar.css';

interface NavbarProps {
    pages: string[],
    onSelect: Function;
}

class Navbar extends React.Component<NavbarProps> {

    constructor(props: NavbarProps) {
        super(props);
    }

    selectNavItem(index: number) {
        console.log("click navbar" + index);
        this.props.onSelect(index);
    }

    createNavbar = () => {
        let children = [];
        for (let i = 0; i < this.props.pages.length; i++) {
            children.push(<li><a href="#" onClick={this.selectNavItem.bind(this, i)}>{this.props.pages[i]}</a></li>);
        }
        return children;
    }

    render() {
        return (
            <div>
                <ul id="nav">
                    { this.createNavbar() }
                </ul>
            </div>
        );
    }
}

export default Navbar;