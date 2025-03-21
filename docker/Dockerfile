# Brug Node.js som base image
FROM node:18 AS build

# Definer arbejdsmappe i containeren
WORKDIR /app

# Kopier package.json og package-lock.json først
COPY calculator-frontend-main/package.json calculator-frontend-main/package-lock.json ./

# Installer afhængigheder
RUN npm install

# Kopier resten af frontend-koden ind i containeren
COPY calculator-frontend-main/ .

# Byg frontend
RUN npm run build

# Brug nginx til at servere den byggede applikation
FROM nginx:alpine

# Kopier de byggede filer fra build-stadiet
COPY --from=build /app/dist/calculator-frontend /usr/share/nginx/html

# Copy the custom nginx.conf file
COPY docker/nginx.conf /etc/nginx/nginx.conf

# Eksponer port 80
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]

