# Otus.Teaching.PromoCodeFactory.Homework.ReactNetCore

## Домашнее задание

#### Создать проект для React.js и связать с проектом по API

##### Цель:

Развернуть ASP.NET Core и React на одном и отдельном хосте.

Развёртывание на одном хосте:

* Создайте новый проект из шаблона ASP.NET Core Web Application with React.js.
* Убедитесь, что в файле Startup.cs добавлено SPA-middleware.
* Запустите ваш проект и убедитесь что фронтенд и бекенд работают правильно.
* Также можете запустить сборку фронтенда отдельно от процесса сборки бекенда. Для этого используйте метод
  UseProxyToSpaDevelopmentServer().

Развёртывание на разных хостах:

* Создайте новый бекенд проект из шаблона ASP.NET Core Web Application with API.
* Создайте новый фронтенд проект с помощью create-react-app.
* На бекенде настройте CORS для адреса вашего фронтенд приложения.
* На фронтенде создайте страницу с отображением погоды. Данные о погоде получайте с вашего бекенд приложения.

