FROM node:18-alpine AS builder
WORKDIR /app
COPY . .
RUN npm install && npm run build -- --configuration production


FROM nginx:alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=builder /app/dist/open-banking-angular-v1 .
COPY ssl/nginx-selfsigned.crt /etc/nginx/certs/nginx-selfsigned.crt
COPY ssl/nginx-selfsigned.key /etc/nginx/certs/nginx-selfsigned.key
COPY nginx.conf /etc/nginx/nginx.conf

ENTRYPOINT ["nginx", "-g", "daemon off;"]
