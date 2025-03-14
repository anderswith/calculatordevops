import http from 'k6/http';


export const options = {
    stages: [
        { duration: '3s', target: 2000 }, //ramp up
        { duration: '3s', target: 2000 }, //stable
        { duration: '3s', target: 0 }, //ramp down
    ]
}
export default () =>{
    http.get('http://localhost:5062/api/Calculator/Calculations')

};

