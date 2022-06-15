FROM node:16.14.0
WORKDIR /app
COPY gallerywebapp/package*.json .
RUN npm install
COPY gallerywebapp/ ./
EXPOSE 3000
CMD ["npm", "start"]