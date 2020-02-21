import React, {useEffect, useState} from 'react';
import axios from "axios";
import {List} from "semantic-ui-react";

interface Value {
    readonly id: number;
    readonly name: string;
}

const App: React.FC = () => {
    const [values, setValues] = useState<Value[]>([]);

    useEffect(() => {
        axios.get<Value[]>("http://localhost:5000/api/values")
            .then(res => setValues(res.data));
    }, []);

    return (
        <div className="App">
            <List>
                {values.map(value => <List.Item key={value.id}>{value.name}</List.Item>)}
            </List>
        </div>
    );
};

export default App;
