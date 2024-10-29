

<template>
  <v-container>
    <v-snackbar v-model="snackbar" :color="color">
      {{ message }}
      <v-btn dark text absolute @click="snackbarAlter(false)"> 
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </v-snackbar>
    <v-row>
      <v-col cols="12">
        <h1 class="display-1 font-weight-bold mb-3">Pessoas</h1>
      </v-col>
      <v-col>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              Novo
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12">
                    <v-text-field
                      v-model="editedItem.nome"
                      label="Nome"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12">
                    <v-text-field
                      v-model="editedItem.telefone"
                      label="Telefone"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12">
                    <v-text-field
                      v-model="editedItem.email"
                      label="Email"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12">
                    <v-text-field
                      v-model="editedItem.dataCadastro"
                      label="DataCadastro"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="dialog = false"
                >Cancel</v-btn
              >
              <v-btn color="blue darken-1" text @click="save">Salvar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-data-table
          :headers="headers"
          :items="pessoas"
          class="elevation-1"
          hide-default-footer
        >
          <!-- <template v-slot:no-data>
            <v-btn color="primary" > Recargar </v-btn>
          </template> -->
          <template v-slot:[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              mdi-pencil
            </v-icon>
            <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>

export default {
  name: "PessoassList",
  data: () => ({
    color: 'primary',
    message: '',         
    dialog: false,
    snackbar: false,
    pessoas: [],
    editedIndex: -1,
    editedItem: {
      nome: "",
      telefone: "",
      email: "",
			dataCadastro: ""
    },

    headers: [
      {
        text: "Nome",
        align: "start",
        sortable: false,
        value: "nome",
      },
      { text: "Telefone", value: "telefone" },
      { text: "Email", value: "email" },
      { text: "DataCadastro", value: "dataCadastro" },
      { text: "Ações", value: "actions", sortable: false },
    ],
  }),
  created() {
    this.getPessoas();
  },

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Novo Item" : "Editando Item";
    },
  },
  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  methods: {
   
    async getPessoas() {

      try {
        let response = await this.$http.get("list/");
        this.pessoas = response.data;       
      } catch (error) {
        //redirect
        this.$router.push({ name: "login" }).catch(() => {});
      }
              

      // this.$http.get("pessoalist/").then((response) => {
      //   this.pessoas = response.data;
      // });
    },

    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.pessoas[this.editedIndex], this.editedItem);

        this.$http.patch("pessoaupdate/" + this.editedItem.id + "/", {
          nome: this.editedItem.nome,
          cpf: this.editedItem.cpf,
          email: this.editedItem.email,
          telefone: this.editedItem.telefone,
        });
      } else {
        this.$http
          .post("pessoacreate/", this.editedItem)
          .then(() => {
            this.pessoas.push(this.editedItem);
            // this.pessoas.push(response.data);
            // this.dialog = false;
            //success
            this.snackbar = true;
            this.message = "Pessoa cadastrada com sucesso!";
            this.color = "success";
          })
          .catch((error) => {
            console.log(error);
          });
      }

      this.dialog = false;
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },
    
    snackbarAlter(bool){
      this.snackbar = bool;
    },

    editItem(item) {
      this.editedIndex = this.pessoas.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.pessoas.indexOf(item);
      confirm("Você tem certeza que deseja deletar este item?") &&
        this.pessoas.splice(index, 1);

      this.$http({
        method: "delete",
        url: "pessoadelete/" + item.id + "/",
      }).then((response) => {
        // pessoa apagada com sucesso
        this.snackbar = true;
        this.message = "Pessoa apagada com sucesso!";
        this.color = "success";
        console.log(response);
      });
    },
  },

  // initialize() {
  //   this.getpessoas();
  // },
};
</script>
