import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [

    {
        id: 17,
        label: 'Users',
        isTitle: true
    },
    {
        id: 4,
        label: 'Vendors',
        icon: 'ri-group-fill',
        link: '/users/users'
    },
    // {
    //     id: 17,
    //     label: 'Pages',
    //     isTitle: true
    // },

    // {
    //     id: 5,
    //     label: 'Settings',
    //     icon: 'ri-settings-2-line',
    //     subItems: [
    //         {
    //             id: 15,
    //             label: 'MENUITEMS.EMAIL.LIST.INBOX',
    //             link: '/email/inbox',
    //             parentId: 14
    //         },
    //         {
    //             id: 16,
    //             label: 'MENUITEMS.EMAIL.LIST.READEMAIL',
    //             link: '/email/read/1',
    //             parentId: 14
    //         }
    //     ]
    // },

];
