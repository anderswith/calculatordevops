import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '3s', target: 2000 }, //ramp up
        { duration: '3s', target: 2000 }, //stable
        { duration: '3s', target: 0 }, //ramp down
    ]
}
export default () =>{
    let res = http.get('http://localhost:5062/api/Calculator/Calculations')
    
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
    
    sleep(1)
};

