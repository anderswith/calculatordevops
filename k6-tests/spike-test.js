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
    let res = http.get('http://79.76.101.254:5000/api/Calculator/Calculations') // udskiftes med staging server url 'http://staging.example.com/api/Calculator/Calculations'
    
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
    
    sleep(1)
};


/*
        workflow
      - name: Setup K6
        run: |
          curl -LO https://github.com/k6io/k6/releases/download/v0.44.0/k6-v0.44.0-linux-amd64.tar.gz
          tar -xvzf k6-v0.44.0-linux-amd64.tar.gz
          sudo mv k6-v0.44.0-linux-amd64/k6 /usr/local/bin/
          k6 version

      - name: Run K6 Spike Test
        run: k6 run k6-tests/spike-test.js
 */