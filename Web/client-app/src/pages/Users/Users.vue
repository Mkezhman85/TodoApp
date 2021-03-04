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
            <q-btn flat round color="primary" icon="mdi-pencil" size="sm">
              <q-tooltip content-style="font-size: 16px" :offset="[10, 10]">
                Редактировать данные пользователя
              </q-tooltip>
            </q-btn>

            <q-btn flat round color="primary" icon="mdi-eye-outline" size="sm">
              <q-tooltip content-style="font-size: 16px" :offset="[10, 10]">
                Просмотреть данные пользователя
              </q-tooltip>
            </q-btn>
          </q-td>
          <q-td key="id" :props="props">
            {{ props.row.id }}
          </q-td>
          <q-td key="login" :props="props">
            {{ props.row.login }}
          </q-td>
          <q-td key="begin_date" :props="props">
            {{ props.row.begin_date }}
          </q-td>
          <q-td key="end_date" :props="props">
            {{ props.row.end_date }}
          </q-td>
          <q-td key="is_blocked" :props="props">
            {{ getBlocked(props.row.is_blocked) }}
          </q-td>
          <q-td key="required_password_change" :props="props">
            {{ props.row.required_password_change }}
          </q-td>
          <q-td key="wrong_login_count" :props="props">
            {{ props.row.wrong_login_count }}
          </q-td>
          <q-td key="last_login_date" :props="props">
            {{ props.row.last_login_date }}
          </q-td>
        </q-tr>
      </template>
    </q-table>
  </div>
</template>

<script lang="ts">
import axios from "axios";
import { Component, Prop, Vue, PropSync, Emit } from "vue-property-decorator";
import _ from "lodash";
import { Dialog } from "quasar";

const columns = [
  { name: "id", align: "center", label: "Id", field: "id", sortable: true },
  {
    name: "login",
    align: "center",
    label: "login",
    field: "login",
    sortable: true,
  },
  {
    name: "begin_date",
    align: "center",
    label: "begin_date",
    field: "begin_date",
    sortable: true,
  },
  {
    name: "end_date",
    align: "center",
    label: "end_date",
    field: "end_date",
    sortable: true,
  },
  {
    name: "is_blocked",
    align: "center",
    label: "is_blocked",
    field: "is_blocked",
    sortable: true,
  },
  {
    name: "required_password_change",
    align: "center",
    label: "required_password_change",
    field: "required_password_change",
    sortable: true,
  },
  {
    name: "wrong_login_count",
    align: "center",
    label: "wrong_login_count",
    field: "wrong_login_count",
    sortable: true,
  },
  {
    name: "last_login_date",
    align: "center",
    label: "last_login_date",
    field: "last_login_date",
    sortable: true,
  },
];

@Component({ components: {} })
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
  async GetUsers() {
    axios.get("https://localhost:24636/api/user/GetUsers").then((res) => {
      // console.log(typeof res.data.data.subjectTableViewModelList);
      this.users = res.data;
      console.log(this.users);
      console.log(res);
      return this.users;
    });
  }
  mounted() {
    this.GetUsers();
  }
}
</script>

<style lang="sass"></style>
