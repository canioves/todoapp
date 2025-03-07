<template>
  <div class="section">
    <div class="container">
      <h1 class="title has-text-centered">TODO App</h1>
      <div class="columns is-centered">
        <div class="column is-half">
          <TodoForm @task-added="fetchTasks" />
          <TodoList :tasks="tasks" @task-deleted="fetchTasks" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent, onMounted, ref, inject } from 'vue'
import TodoForm from './components/TodoForm.vue'
import TodoList from './components/TodoList.vue'

export default defineComponent({
  components: {
    TodoForm,
    TodoList,
  },
  setup() {
    const $http = inject('$http')
    const tasks = ref([])
    const fetchTasks = async () => {
      const response = await $http.get('/tasks')
      if (Array.isArray(response.data)) {
        tasks.value = response.data
      }
      else {
        console.error("API вернул не массив")
        tasks.value = []
      }
    }

    onMounted(fetchTasks)

    return {
      tasks,
      fetchTasks,
    }
  },
})
</script>

<style>
.section {
  padding: 2rem 0;
}

.container {
  max-width: 800px;
}
</style>