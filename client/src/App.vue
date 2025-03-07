<template>
  <div>
    <h1>TODO App</h1>
    <TodoForm @task-added="fetchTasks" />
    <TodoList :tasks="tasks" @task-deleted="fetchTasks" />
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
