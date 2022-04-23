import React from 'react';
import "./InventoryModule.css"

export default class InventorySlot extends React.Component<
    {
        index: number;
        image: string;
        label: string;
    },
    {
        hovered: boolean;
        dragging: boolean;
    }
> {
    constructor(props: any) {
        super(props);
        this.state = {
            hovered: false,
            dragging: false
        };
    }

    render() {
        return (
            <div
                key={`slot-${this.props.index}`}
                className="flex inventory-slot"
                onMouseEnter={() => {
                    this.setState({ hovered: true });
                }}
                onMouseLeave={() => {
                    this.setState({ hovered: false });
                }}
                onClick={() => {
                    console.log("clicked");
                }}
                style={{
                    backgroundColor:
                        this.state.hovered && !this.state.dragging
                            ? "#505050"
                            : "black"
                }}
            >
                <div>
                    <img src="https://d1kmrw706gcgzi.cloudfront.net/catalog/product/placeholder/default/Image-required.png" width="50px" height="50px" alt="" />
                </div>
                <p>{this.props.label}</p>
                <div className="inventory-slot-weight-count">{`${1} (${1.0})`}</div>
            </div>
        );
    }
}