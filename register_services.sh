#!/bin/sh
set -e

# Kong Admin API URL inside Docker network
KONG_ADMIN_URL="http://kong:8001"

echo "🔍 Checking Kong Admin API..."
curl -s $KONG_ADMIN_URL/status || { echo "❌ Cannot reach Kong"; exit 1; }
echo "✅ Kong reachable."

# List of services as a plain string with line breaks
SERVICES="
account-service|http://account-service:8088|/account
atm-service|http://atm-service:8082|/atm
branch-service|http://branch-service:8084|/branch
card-service|http://card-service:8086|/card
authenticator-service|http://authenticator-service:8090|/auth
notification-service|http://notification-service:8095|/notifications
"

# Loop over each line in SERVICES
echo "$SERVICES" | while IFS='|' read SERVICE_NAME UPSTREAM_URL ROUTE_PATH; do
    echo "📦 Creating service: $SERVICE_NAME"
    curl -s -X POST $KONG_ADMIN_URL/services \
        --data name="$SERVICE_NAME" \
        --data url="$UPSTREAM_URL"

    echo "🛣 Creating route: $ROUTE_PATH"
    curl -s -X POST $KONG_ADMIN_URL/services/"$SERVICE_NAME"/routes \
        --data name="${SERVICE_NAME}-route" \
        --data paths[]="$ROUTE_PATH"

    echo "✅ $SERVICE_NAME registered with route $ROUTE_PATH"
done

echo "🚀 All services registered successfully!"
