import { Home } from '../components/Home';
import { User } from './User/User';
import { Food } from './Food/Food';
import { ForbiddenFood } from './ForbiddenFood/ForbiddenFood';
import AppInfo from '../components/AppInfo/AppInfo';
import Recipes from './Recipes/Recipes';
import FastingGuide from './FastingGuide/FastingGuide';
import EatingGuide from './EatingGuide/EatingGuide';
import News from './News/News';

const dashboardRoutes = [
    {
        path: "/",
        name: "Home",
        rtlName: "لوحة القيادة",
        icon: Home,
        component: Home,
        layout: "/Layout"
    },
    {
        path: "/user",
        name: "Usuarios",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: User,
        layout: "/Layout"
    },
    {
        path: "/appInfo",
        name: "Informacion de la App",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: AppInfo,
        layout: "/Layout"
    },
    {
        path: "/food",
        name: "Alimentos Permitidos",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: Food,
        layout: "/Layout"
    },
    {
        path: "/forbiddenFood",
        name: "Alimentos No Permitidos",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: ForbiddenFood,
        layout: "/Layout"
    },
    {
        path: "/recipes",
        name: "Recetas",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: Recipes,
        layout: "/Layout"
    },
    {
        path: "/fastingGuide",
        name: "Guias de Ayuno",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: FastingGuide,
        layout: "/Layout"
    },
    {
        path: "/eatingGuide",
        name: "Guias Alimenticias",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: EatingGuide,
        layout: "/Layout"
    },
    {
        path: "/news",
        name: "Noticias",
        rtlName: "قائمة الجدول",
        icon: "content_paste",
        component: News,
        layout: "/Layout"
    }
];

export default dashboardRoutes;
