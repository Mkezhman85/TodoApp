<template>
  <div>
    <q-table
      title="Перечень пользователей"
      :data="users"
      :columns="columns"
      row-key="id"
      style="height: calc(100vh - 50px)"
      separator="cell"
      :filter="filter"
      :rowsPerPage="[]"
      :pagination.sync="pagination"
      class="my-sticky-virtscroll-table"
      virtual-scroll
      :virtual-scroll-sticky-size-start="48"
      flat
      bordered
    >
      <template v-slot:top-right>
        <q-btn
          color="secondary"
          label="Добавить пользователя"
          class="q-mx-sm"
          icon-right="mdi-plus"
          no-caps
          @click="openUserEditor"
        />
        <q-btn
          color="primary"
          label="Выгрузить csv"
          class="q-mx-sm"
          icon-right="archive"
          no-caps
        />

        <q-input
          borderless
          dense
          debounce="300"
          v-model="filter"
          placeholder="Поиск"
          outlined
          clearable
        >
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
      </template>

      <template v-slot:header="props">
        <q-tr :props="props">
          <q-th auto-width class="bg-primary text-white">Действия</q-th>
          <q-th
            v-for="col in props.cols"
            :key="col.name"
            :props="props"
            class="bg-primary text-white"
          >
            {{ col.label }}
          </q-th>
        </q-tr>
      </template>

      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td auto-width>
            <q-btn
              flat
              round
              color="primary"
              icon="edit"
              size="sm"
              @click="editUserOpen(props.row.id)"
            >
              <q-tooltip content-style="font-size: 16px" :offset="[10, 10]">
                Редактировать данные пользователя
              </q-tooltip>
            </q-btn>

            <q-btn
              flat
              round
              color="negative"
              icon="delete"
              size="sm"
              @click="DeleteUserById(props.row.id)"
            >
              <q-tooltip content-style="font-size: 16px" :offset="[10, 10]">
                Удалить пользователя
              </q-tooltip>
            </q-btn>

            <q-btn
              flat
              round
              color="primary"
              icon="visibility"
              size="sm"
              @click="viewUserDetails(props.row.id)"
            >
              <q-tooltip content-style="font-size: 16px" :offset="[10, 10]">
                Просмотр данных пользователя
              </q-tooltip>
            </q-btn>
          </q-td>
          <q-td key="id" :props="props">
            {{ props.row.id }}
          </q-td>
          <q-td key="login" :props="props">
            {{ props.row.login }}
          </q-td>
          <q-td key="beginDate" :props="props">
            {{ FormatDate(props.row.beginDate) }}
          </q-td>
          <q-td key="endDate" :props="props">
            {{ FormatDate(props.row.endDate) }}
          </q-td>
          <q-td key="isBlocked" :props="props">
            {{ getBlocked(props.row.isBlocked) }}
          </q-td>
          <q-td key="requiredPasswordChange" :props="props">
            {{ requiredPasswordChange(props.row.requiredPasswordChange) }}
          </q-td>
          <q-td key="wrongLoginCount" :props="props">
            {{ props.row.wrongLoginCount }}
          </q-td>
          <q-td key="lastLoginDate" :props="props">
            {{ FormatDate(props.row.lastLoginDate) }}
          </q-td>
        </q-tr>
      </template>
    </q-table>
    <UserEditor
      v-model="userEditorOpened"
      @open-userEditorOpened="openUserEditor"
      @hide-userEditorOpened="hideUserEditor"
      :user="selectedUser"
      @create-newUser="CreateNewUser"
      :readonlyParam="readonlyParam"
    />
  </div>
</template>

<script lang="ts">
import axios from "axios";
import { Component, Prop, Vue, PropSync, Emit } from "vue-property-decorator";
import _ from "lodash";

import UserEditor from "./UserEditor.vue";
import { TUser } from "src/Models/Users";

import { Dialog } from "quasar";
import { Notify } from "quasar";

const columns = [
  {
    name: "id",
    align: "center",
    label: "id пользователя в базе",
    field: "id",
    sortable: true,
  },
  {
    name: "login",
    align: "center",
    label: "Логин",
    field: "login",
    sortable: true,
  },
  {
    name: "beginDate",
    align: "center",
    label: "Дата начала доступа",
    field: "beginDate",
    sortable: true,
  },
  {
    name: "endDate",
    align: "center",
    label: "Дата окончания доступа",
    field: "endDate",
    sortable: true,
  },
  {
    name: "isBlocked",
    align: "center",
    label: "Заблокирован",
    field: "isBlocked",
    sortable: true,
  },
  {
    name: "requiredPasswordChange",
    align: "center",
    label: "Требуется смена пароля",
    field: "requiredPasswordChange",
    sortable: true,
  },
  {
    name: "wrongLoginCount",
    align: "center",
    label: "Количество проваленных авторизаций",
    field: "wrongLoginCount",
    sortable: true,
  },
  {
    name: "lastLoginDate",
    align: "center",
    label: "Дата и время последней авторизации",
    field: "lastLoginDate",
    sortable: true,
  },
];

@Component({ components: { UserEditor } })
export default class Users extends Vue {
  filter = "";
  splitterModel = 15;
  pagination = {
    rowsPerPage: 15,
  };

  users = [];

  columns = columns;

  getBlocked(is_blocked: boolean) {
    if (is_blocked) {
      return "Да";
    } else {
      return "Нет";
    }
  }

  requiredPasswordChange(required_password_change: boolean) {
    if (required_password_change) {
      return "Да";
    } else {
      return "Нет";
    }
  }

  async GetUsers() {
    axios.get("https://localhost:24636/api/user/GetUsers").then((res) => {
      // console.log(typeof res.data.data.subjectTableViewModelList);
      this.users = res.data;
      console.log(this.users);
      return this.users;
    });
  }

  FormatDate(date: Date) {
    return (
      new Date(date).getDate() +
      "." +
      Number(new Date(date).getMonth() + 1) +
      "." +
      new Date(date).getFullYear()
    );
  }

  async CreateNewUser(user: TUser) {
    console.log(user);
    const myUser: TUser = {
      login: user.login,
      isBlocked: Boolean(user.isBlocked),
      requiredPasswordChange: Boolean(user.requiredPasswordChange),
      wrongLoginCount: Number(user.wrongLoginCount),
      beginDate: new Date(user.beginDate),
      endDate: new Date(user.endDate),
      lastLoginDate: new Date(user.lastLoginDate),
    };

    axios.post("https://localhost:24636/api/user/CreateNewUser", myUser).then((res) => {
      this.$q.notify({
        message: "Пользователь с логином " + myUser.login + " успешно добавлен. ",
        icon: "done",
        position: "top-right",
        color: "positive",
      });

      this.hideUserEditor();
      this.GetUsers();
    });
  }

  readonlyParam = false;
  selectedUser: object = {};
  viewUserDetails(id: number) {
    this.userEditorOpened = true;
    this.selectedUser = _.find(this.users, { id: id });
    this.readonlyParam = true;
  }

  editUserOpen(id: number) {
    this.userEditorOpened = true;
    this.selectedUser = _.find(this.users, { id: id });
  }

  // Удаление пользователя
  DeleteUserById(id: number) {
    this.$q
      .dialog({
        title: "Подтверждение удаления",
        message: "Вы действительно хотите удалить информацию о пользователе?",
        cancel: true,
        persistent: true,
      })
      .onOk(() => {
        axios({
          method: "delete",
          url: "https://localhost:24636/api/user/DeleteUserById",
          params: {
            id: id,
          },
        }).then((res) => {
          this.$q.notify({
            message: "Пользователь с id " + id + " удален.",
            icon: "done",
            position: "top-right",
            color: "positive",
          });
          this.GetUsers();
        });
      });
  }

  confirmDelete() {
    this.$q
      .dialog({
        title: "Подтверждение удаления",
        message: "Вы действительно хотите удалить информацию о пользователе?",
        cancel: true,
        persistent: true,
      })
      .onOk(() => {})
      .onOk(() => {
        // console.log('>>>> second OK catcher')
      })
      .onCancel(() => {
        // console.log('>>>> Cancel')
      })
      .onDismiss(() => {
        // console.log('I am triggered on both OK and Cancel')
      });
  }

  userEditorOpened = false;
  openUserEditor() {
    this.readonlyParam = false;
    this.selectedUser = {}
    this.userEditorOpened = true;
    console.log(this.userEditorOpened);
  }

  hideUserEditor() {
    this.userEditorOpened = false;
    this.readonlyParam = false;
    console.log(this.userEditorOpened);
  }

  mounted() {
    this.GetUsers();
  }
}
</script>

<style lang="sass">
.my-sticky-virtscroll-table
  /* height or max-height is important */
  height: 410px

  .q-table__top,
  .q-table__bottom,
  thead tr:first-child th /* bg color is important for th; just specify one */
    background-color: #fff

  thead tr th
    position: sticky
    z-index: 1
  /* this will be the loading indicator */
  thead tr:last-child th
    /* height of all previous header rows */
    top: 48px
  thead tr:first-child th
    top: 0
</style>
