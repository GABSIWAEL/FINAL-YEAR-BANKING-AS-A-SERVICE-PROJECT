FROM node:18-alpine AS builder
WORKDIR /app
COPY . .
RUN npm install && npm run build -- --configuration production


FROM nginx:alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=builder /app/dist/open-banking-angular-v1 .
COPY ssl/openbanking.local.pem /etc/nginx/certs/openbanking.local.pem
COPY ssl/openbanking.local-key.pem /etc/nginx/certs/openbanking.local-key.pem

COPY nginx.conf /etc/nginx/nginx.conf

ENTRYPOINT ["nginx", "-g", "daemon off;"]
