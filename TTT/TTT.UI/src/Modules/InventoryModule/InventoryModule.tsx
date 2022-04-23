import React from 'react';
import ResponsiveGridLayout from 'react-grid-layout';
import { BaseModule } from '../../Base/BaseModule';
import './InventoryModule.css';

interface InventoryModuleProps { }

interface InventoryModuleState {
    items: any[];
    maxSlots: number;
    maxRows: number;
}

export default class InventoryModule extends BaseModule<InventoryModuleProps, InventoryModuleState> {
    public moduleName: string;

    constructor(props: InventoryModuleProps) {
        super(props);

        this.moduleName = "inventory";

        this.state = {
            items: [
                {
                    Id: 1,
                    Name: "TestBla",
                    Quantity: 10
                },
                {
                    Id: 2,
                    Name: "test",
                    Quantity: 1
                },
                {
                    Id: 3,
                    Name: "basd",
                    Quantity: 5
                },
                {
                    Id: 4,
                    Name: "fdsgfgd",
                    Quantity: 8
                },
                {
                    Id: 5,
                    Name: "zutruetz",
                    Quantity: 15
                },
                {
                    Id: 6,
                    Name: "vcbncvbn",
                    Quantity: 105
                },
            ],
            maxSlots: 25,
            maxRows: 5
        }
    }

    public render(): JSX.Element {
        var layout = this.generateLayout();
        console.log(layout);
        return (
            <div className="flex items-center justify-center h-screen">
                <div className="inventory card text-center shadow-2xl">
                    <ResponsiveGridLayout className="layout" layout={layout} cols={6} width={500} containerPadding={[25, 25]} onDragStart={() => { console.log("layout changed") }}>
                        {
                            this.state.items.map(item => {
                                return (
                                    <div key={item.Id} className="bg-red-600">
                                        <span className="text">{item.Name}</span>
                                    </div>
                                );
                            })
                        }
                    </ResponsiveGridLayout>
                </div>
            </div >
        );
    }

    generateLayout() {
        return this.state.items.map((item, i) => {
            return {
                x: (i * 2) % 8,
                y: (i * 2) % 8,
                w: 2,
                h: 1,
                i: (i + 1).toString(),
                isBounded: true,
            };
        });
    }

    public moduleInit(): void {
        throw new Error('Module not implemented.');
    }
}

