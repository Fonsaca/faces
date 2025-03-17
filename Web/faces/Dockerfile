# Step 1: Build the Angular app
FROM node:18 AS build-stage

# Set the working directory
WORKDIR /app

# Copy the package.json and package-lock.json files
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the Angular application files
COPY . .

# Build the Angular application
RUN npm run build --prod

# Step 2: Serve the Angular app using a lightweight server
FROM nginx:alpine AS production-stage

# Copy the built app from the previous stage
COPY --from=build-stage /app/dist/faces/browser /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx to serve the Angular app
CMD ["nginx", "-g", "daemon off;"]